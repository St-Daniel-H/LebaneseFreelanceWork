"use client";
import React, { useState, useEffect } from "react";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";

export default function Notifications() {
  const [conn, setConn] = useState<HubConnection | null>(null); // Define type for conn

  const userId: number = 5; // Define type for userId

  useEffect(() => {
    const startConnection = async () => {
      try {
        const connection = new HubConnectionBuilder()
          .withUrl("https://localhost:7170/Hubs/NotificationHub")
          .build();

        connection.on("ReceiveNotification", (notification) => {
          console.log("Received notification:", notification);
          // You can update state, show a notification, etc.
        });

        await connection.start();
        console.log("SignalR Connected");
        setConn(connection); // Update state with the connection
      } catch (err) {
        console.error("SignalR Connection Error:", err);
      }
    };

    startConnection();

    // Cleanup function
    return () => {
      if (conn) {
        conn.stop(); // Stop the connection when the component unmounts
      }
    };
  }, []); // Empty dependency array ensures this effect runs once when component mounts

  return <h1>Notifications</h1>;
}
