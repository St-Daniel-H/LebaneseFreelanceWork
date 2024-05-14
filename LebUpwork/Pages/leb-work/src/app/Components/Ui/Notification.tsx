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
  const token =
    localStorage.getItem("token") || sessionStorage.getItem("token") || "";
  const startConnection = async () => {
    try {
      console.log(token);
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

      await connection.start().catch((e) => console.log("error, " + e));
      console.log("SignalR Connected");
      setConn(connection); // Update state with the connection
    } catch (err) {
      console.error("SignalR Connection Error:", err);
    }
  };
  useEffect(() => {
    // startConnection();
    startConnection();
    return () => {
      if (conn) {
        conn.stop(); // Stop the connection when the component unmounts
      }
    };
  }, []);

  return (
    <h1>
      Notifications:
      {notifications.map((notification, index) => (
        <li key={index}>{notification}</li>
      ))}
    </h1>
  );
}
