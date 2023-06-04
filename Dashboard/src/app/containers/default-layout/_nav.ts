import { INavData } from "@coreui/angular";

export const navItems: INavData[] = [
  {
    name: "Dashboard",
    url: "/dashboard",
    iconComponent: { name: "cil-speedometer" },
  },
  {
    name: "Departments",
    url: "/Departments",
    iconComponent: { name: "cil-home" },
  },
  {
    name: "Doctors",
    url: "/Doctors",
    iconComponent: { name: "cil-people" },
  },
  {
    name: "Patients",
    url: "/Patients",
    iconComponent: { name: "cil-user" },
  },
  {
    name: "Reservations",
    url: "/Reservations",
    iconComponent: { name: "cil-task" },
  },
  {
    name: "Banner",
    url: "/Banners",
    iconComponent: { name: "cilAudioDescription" },
  },
  {
    name: "Camp Image",
    url: "/CampImages",
    iconComponent: { name: "cilImage" },
  },
  {
    name: "Messages",
    url: "/Messages",
    iconComponent: { name: "cilCommentBubble" },
  },
];
