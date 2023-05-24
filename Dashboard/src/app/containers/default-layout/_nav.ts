import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    iconComponent: { name: 'cil-speedometer' }
  },
  {
    name: 'Departments',
    url: '/departments',
    iconComponent: { name: 'cil-home' }
  },
  {
    name: 'Doctors',
    url: '/Doctors',
    iconComponent: { name: 'cil-people' }
  },
  {
    name: 'Patients',
    url: '/patients',
    iconComponent: { name: 'cil-user' }
  },
  {
    name: 'Reservations',
    url: '/reservations',
    iconComponent: { name: 'cil-task' }
  },
];
