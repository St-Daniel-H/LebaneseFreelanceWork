"use client";
import "@/app/SCSS/TopBar.scss";
import { IoIosNotifications } from "react-icons/io";
import { useState } from "react";
import { CiMenuBurger } from "react-icons/ci";

function TopBar() {
  const [showNot, setShowNot] = useState(false);

  return (
    <div id="TopBar">
      <div>
        <div
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
          }}
        >
          <CiMenuBurger
            style={{ marginRight: "10px", cursor: "pointer" }}
            id="burgerMenu"
          />
          <h1>LebanWork</h1>
        </div>
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
