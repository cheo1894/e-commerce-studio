import Spacer from "@/Shared/Components/Spacer";
import LinkTo from "@/Shared/Components/LinkTo";
import SidebarMenu from "@/Shared/Components/SidebarMenu";
import useLogout from "@/Modules/Auth/Presentation/hooks/useLogout";

type Props = {};

type buttonprops = {
  name: string;
  to?: string;
  subroutes?: buttonprops[];
};

function index({}: Props) {
  const { logout } = useLogout();

  const ds = "dashboard";

  const buttonList: buttonprops[] = [
    {
      name: "Products",
      subroutes: [
        { name: "Products", to: "/dashboard/products" },
        { name: "Categories", to: "/dashboard/products/categories" },
      ],
    },
    {
      name: "Users",
      subroutes: [
        { name: "Users", to: "/dashboard/users" },
        { name: "Roles", to: "/dashboard/users/roles" },
      ],
    },
  ];
  return (
    <>
      <div className={ds + "-sidebar"}>
        <h1>CMS</h1>
        <SidebarMenu buttonList={buttonList} />
        <Spacer type="column" />
        <LinkTo to="" title="Salir" onClick={logout} />
      </div>
    </>
  );
}

export default index;
