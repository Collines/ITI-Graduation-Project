import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import{DoctorComponent} from './components/doctor/doctor.component';
import{DoctordetailssComponent} from './components/doctordetailss/doctordetailss.component'
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AboutComponent } from './components/about/about.component';
import { DepartmentsComponent } from './components/departments/departments.component';
import { ContactComponent } from './components/contact/contact.component';
import { ReservationComponent } from './components/reservation/reservation.component';
const routes: Routes = [
  {path:"",component:HomeComponent},
  {path:"home", component:HomeComponent},
  {path:"about", component:AboutComponent},
  {path:"register", component:RegisterComponent},
  {path:"login", component:LoginComponent},
  {path:"doctor",component:DoctorComponent},
  {path:"doctor/:id",component:DoctordetailssComponent},
  {path:"dashboard",component:DashboardComponent},
  {path: 'departments',component: DepartmentsComponent},
  {path:"contact",component:ContactComponent},
  {path:"reservation",component:ReservationComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
