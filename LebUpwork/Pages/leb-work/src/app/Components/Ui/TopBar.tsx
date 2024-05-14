"use client";
import "@/app/SCSS/TopBar.scss";
import { IoIosNotifications } from "react-icons/io";
import { useState } from "react";
function TopBar() {
  const [showNot, setShowNot] = useState(false);

  return (
    <div id="TopBar">
      <div>
        <h1>LebanWork</h1>
      </div>
      <div>
        <IoIosNotifications
          onClick={() => setShowNot(!showNot)}
          style={{ fontSize: "25px", cursor: "pointer" }}
        />
      </div>
    </div>
  );
}
export default TopBar;
