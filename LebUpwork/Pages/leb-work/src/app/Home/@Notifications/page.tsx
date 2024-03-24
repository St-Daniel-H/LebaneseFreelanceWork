"use client";
import React, { useState, useEffect } from "react";
import {
  HubConnection,
  HubConnectionBuilder,
  HttpTransportType,
} from "@microsoft/signalr";

export default function Notifications() {
  const [conn, setConn] = useState<HubConnection | null>(null); // Define type for conn
  const [notifications, setNotifications] = useState<string[]>([]); // State to store notifications

  const userId: number = 5; // Define type for userId
  const token =
    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1IiwianRpIjoiOTQ4MTQxMGYtZmQyZi00YjNhLTg5YzUtNWEwYjMxZDM3ZjlmIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiI1IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiNSIsImV4cCI6MTcxMzg3NzU1NCwiaXNzIjoiRGFuaWVsIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo3MTQwIn0.nEhVw2ctXMjixcPPnyxIs_IFhPvkPOMI7-88Li4_6Uc";
  const startConnection = async () => {
    try {
      const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:7170/Hubs/NotificationHub", {
          accessTokenFactory: () => token,
          skipNegotiation: true,
          transport: HttpTransportType.WebSockets,
        })
        .build();

      connection.on("ReceiveNotification", (notification) => {
        console.log("Received notification:", notification);
        // You can update state, show a notification, etc.
        setNotifications((prevNotifications) => [
          ...prevNotifications,
          notification,
        ]);
      });

      await connection.start();
      console.log("SignalR Connected");
      setConn(connection); // Update state with the connection
    } catch (err) {
      console.error("SignalR Connection Error:", err);
    }
  };
  useEffect(() => {
    // startConnection();
    startConnection();
    // Cleanup function
    return () => {
      if (conn) {
        conn.stop(); // Stop the connection when the component unmounts
      }
    };
  }, []); // Empty dependency array ensures this effect runs once when component mounts

  return (
    <h1>
      Notifications:
      {notifications.map((notification, index) => (
        <li key={index}>{notification}</li>
      ))}
    </h1>
  );
}
