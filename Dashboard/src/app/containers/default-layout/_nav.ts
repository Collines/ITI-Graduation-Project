import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    iconComponent: { name: 'cil-speedometer' }
  },
  {
    name: 'Departments',
    url: '/Departments',
    iconComponent: { name: 'cil-home' }
  },
  {
    name: 'Doctors',
    url: '/Doctors',
    iconComponent: { name: 'cil-people' }
  },
  {
    name: 'Patients',
    url: '/Patients',
    iconComponent: { name: 'cil-user' }
  },
  {
    name: 'Reservations',
    url: '/reservations',
    iconComponent: { name: 'cil-task' }
  },
  {
    name: 'Pages',
    url: '/login',
    iconComponent: { name: 'cil-star' },
    children: [
      {
        name: 'Login',
        url: '/login'
      },
      {
        name: 'Register',
        url: '/register'
      },
      {
        name: 'Error 404',
        url: '/404'
      },
      {
        name: 'Error 500',
        url: '/500'
      }
    ]
  },
];
