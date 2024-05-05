"use client";

import TopBar from "../Components/Ui/TopBar";
import UserProfilePop from "../Components/Ui/UserProfilePop";
export default function Home() {
  return (
    <div id="Home">
      <TopBar />
      <div id="HomeBody">
        <UserProfilePop />
      </div>
    </div>
  );
}
