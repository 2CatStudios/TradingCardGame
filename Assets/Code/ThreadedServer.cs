using System;
using System.Net;
using UnityEngine;
using System.Threading;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
//Written by Michael Bethke

public class ThreadedServer : MonoBehaviour
{

	private Socket _serverSocket;
	private int _port;
	
	public ThreadedServer(int port)
	{
	
		_port = port;
	}
	
	private class ConnectionInfo
	{
	
		public Socket Socket;
		public Thread Thread;
	}
	
	private Thread _acceptThread;
	private List<ConnectionInfo> _connections = new List<ConnectionInfo>();
	
	
	public void Start()
	{
	
		SetupServerSocket();
		_acceptThread = new Thread(AcceptConnections);
		_acceptThread.IsBackground = true;
		_acceptThread.Start();
	}
	
	
	private void SetupServerSocket()
	{
	
		// Resolving local machine information
		IPHostEntry localMachineInfo = Dns.GetHostEntry(Dns.GetHostName());
		IPEndPoint myEndpoint = new IPEndPoint( localMachineInfo.AddressList[0], _port);
	
		// Create the socket, bind it, and start listening
		_serverSocket = new Socket(myEndpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
		_serverSocket.Bind(myEndpoint);
		_serverSocket.Listen((int)SocketOptionName.MaxConnections);
	}
	
	
	private void AcceptConnections()
	{
	
		while (true)
		{
		
			// Accept a connection
			Socket socket = _serverSocket.Accept();
			ConnectionInfo connection = new ConnectionInfo();
			connection.Socket = socket;
		
			// Create the thread for the receives.
			connection.Thread = new Thread(ProcessConnection);
			connection.Thread.IsBackground = true;
			connection.Thread.Start(connection);
		
			// Store the socket
			lock (_connections) _connections.Add(connection);
		}
	}
	
	
	private void ProcessConnection(object state)
	{
	
		ConnectionInfo connection = (ConnectionInfo)state;
		byte[] buffer = new byte[255];
	
		try
		{
		
			while (true)
			{
			
				int bytesRead = connection.Socket.Receive(buffer);
			
				if (bytesRead > 0)
				{
				
					lock (_connections)
					{
					
						foreach (ConnectionInfo conn in _connections)
						{
						
							if (conn != connection)
							{
							
								conn.Socket.Send( buffer, bytesRead, SocketFlags.None);
							}
						}
					}
				} else if (bytesRead == 0)
					return;
			
			}
		} catch (SocketException exc)
		{
		
			UnityEngine.Debug.Log ("Socket exception: " + exc.SocketErrorCode);
		} catch (Exception exc)
		{
		
			UnityEngine.Debug.Log ("Exception: " + exc);
		} finally
		{
		
			connection.Socket.Close();
			lock (_connections) _connections.Remove(connection);
		}
	}
}

class AsynchronousIoServer
{
	
	public AsynchronousIoServer(int port)
	{
	
		_port = port;
	}
	
	private Socket _serverSocket;
	private int _port;
	
	private class ConnectionInfo
	{
		
		public Socket Socket;
		public byte[] Buffer;
	}
	
	private List<ConnectionInfo> _connections = new List<ConnectionInfo>();
	
	
	public void Start()
	{
		
		SetupServerSocket();
		
		for (int i = 0; i < 10; i++)
			_serverSocket.BeginAccept( new AsyncCallback(AcceptCallback), _serverSocket);
		
	}
	
	
	private void SetupServerSocket()
	{
	
		// Resolving local machine information
		IPHostEntry localMachineInfo = Dns.GetHostEntry(Dns.GetHostName());
		IPEndPoint myEndpoint = new IPEndPoint( localMachineInfo.AddressList[0], _port);
	
		// Create the socket, bind it, and start listening
		_serverSocket = new Socket(myEndpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
		_serverSocket.Bind(myEndpoint);
		_serverSocket.Listen((int)SocketOptionName.MaxConnections);
	}
	
	
	private void AcceptCallback(IAsyncResult result)
	{
		
		ConnectionInfo connection = new ConnectionInfo();
		
		try
		{
			
			// Finish Accept
			
			Socket s = (Socket)result.AsyncState;
			connection.Socket = s.EndAccept(result);
			connection.Buffer = new byte[255];
			lock (_connections) _connections.Add(connection);
			
			// Start Receive and a new Accept
			connection.Socket.BeginReceive(connection.Buffer, 0, connection.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), connection);
			_serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), result.AsyncState);
		} catch (SocketException exc)
		{
			
			CloseConnection(connection);
			Console.WriteLine("Socket exception: " + exc.SocketErrorCode);
		} catch (Exception exc)
		{
			
			CloseConnection(connection);
			Console.WriteLine("Exception: " + exc);
		}
	}
	
	
	private void ReceiveCallback(IAsyncResult result)
	{
		
		ConnectionInfo connection = (ConnectionInfo)result.AsyncState;
		
		try
		{
			
			int bytesRead = connection.Socket.EndReceive(result);
			if (0 != bytesRead)
			{
				
				lock (_connections)
				{
					
					foreach (ConnectionInfo conn in _connections)
					{
						
						if (connection != conn)
						{
							
							conn.Socket.Send(connection.Buffer, bytesRead, SocketFlags.None);
						}
					}
				}
			
				connection.Socket.BeginReceive(connection.Buffer, 0, connection.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), connection);
			} else CloseConnection(connection);
		} catch (SocketException exc)
		{
			
			CloseConnection(connection);
			Console.WriteLine("Socket exception: " + exc.SocketErrorCode);
		} catch (Exception exc)
		{
			
			CloseConnection(connection);
			Console.WriteLine("Exception: " + exc);
		}
	}
	
	private void CloseConnection(ConnectionInfo ci)
	{
		
		ci.Socket.Close();
		lock (_connections) _connections.Remove(ci);
	}
}