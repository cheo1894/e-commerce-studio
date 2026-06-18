import "./index.css";

import DashboardSideBar from "../../Components/DashboardSideBar";
import DashboardContent from "../../Components/DashboardContent";
import { Outlet } from "react-router";

type Props = {};

function Index({}: Props) {
  const ds = "dashboard";
  return (
    <div className={ds + "-container"}>
      {/* <DashboardHeader></DashboardHeader> */}
      <div className={ds + "-horizontal"}>
        <DashboardSideBar />
        <DashboardContent>
          <div className="dashboard-content-inner">
            <Outlet />
          </div>
        </DashboardContent>
      </div>
    </div>
  );
}

export default Index;
