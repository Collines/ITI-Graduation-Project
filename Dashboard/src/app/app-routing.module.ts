import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";


import { DefaultLayoutComponent } from './containers';
import { Page404Component } from './views/pages/page404/page404.component';
import { Page500Component } from './views/pages/page500/page500.component';
import { LoginComponent } from './views/pages/login/login.component';
import { AllDoctorsComponent } from './views/doctors-views/all-doctors/all-doctors.component';
import { AddDoctorComponent } from './views/doctors-views/add-doctor/add-doctor.component';
import { EditDoctorComponent } from "./views/doctors-views/edit-doctor/edit-doctor.component";
import { DetailsDoctorComponent } from "./views/doctors-views/details-doctor/details-doctor.component";

const routes: Routes = [
  {
    path: "",
    redirectTo: "dashboard",
    pathMatch: "full",
  },
  {
    path: "",
    component: DefaultLayoutComponent,
    data: {
      title: "Home",
    },
    children: [
      {
        path: "dashboard",
        loadChildren: () =>
          import("./views/dashboard/dashboard.module").then(
            (m) => m.DashboardModule
          ),
      },
      {
        path: 'Doctors',
        component: AllDoctorsComponent
      },
      {
        path: 'AddDoctor',
        component: AddDoctorComponent
      },
      {
        path: 'EditDoctor/:id',
        component: EditDoctorComponent
      },
      {
        path: 'Doctors/:id',
        component: DetailsDoctorComponent
      },
      {
        path: "pages",
        loadChildren: () =>
          import("./views/pages/pages.module").then((m) => m.PagesModule),
      },
    ],
  },
  {
    path: "404",
    component: Page404Component,
    data: {
      title: "Page 404",
    },
  },
  {
    path: "500",
    component: Page500Component,
    data: {
      title: "Page 500",
    },
  },
  {
    path: "login",
    component: LoginComponent,
    data: {
      title: "Login Page",
    },
  },
  { path: "**", redirectTo: "dashboard" },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: "top",
      anchorScrolling: "enabled",
      initialNavigation: "enabledBlocking",
      // relativeLinkResolution: 'legacy'
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
