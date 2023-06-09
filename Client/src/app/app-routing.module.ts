import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { DoctorComponent } from './components/doctor/doctor.component';
import { DoctordetailsComponent } from './components/doctordetails/doctordetails.component';
import { ProfileComponent } from './components/profile/profile.component';
import { AboutComponent } from './components/about/about.component';
import { DepartmentsComponent } from './components/departments/departments.component';
import { ContactComponent } from './components/contact/contact.component';
import { ReservationsComponent } from './components/reservations/reservations.component';
import { ReservationComponent } from './components/reservation/reservation.component';
import { DepartmentComponent } from './components/department/department.component';
import { DepartmentDetailComponent } from './components/department-detail/department-detail.component';
import { NotfoundComponent } from './components/notfound/notfound.component';
import { SettingsComponent } from './components/settings/settings.component';
import { NewsComponent } from './components/news/news.component';
import { NewsDetailComponent } from './components/news-detail/news-detail.component';
const routes: Routes = [
  { path: '', redirectTo:'home', pathMatch:'full' },
  { path: 'home', component: HomeComponent, data: { title: 'Home' } },
  { path: 'about', component: AboutComponent, data: { title: 'About' } },
  {
    path: 'register',
    component: RegisterComponent,
    data: { title: 'Register' },
  },
  { path: 'login', component: LoginComponent, data: { title: 'Login' } },
  { path: 'doctor', component: DoctorComponent },
  {
    path: 'doctor/:id',
    component: DoctordetailsComponent,
    data: { title: 'Doctor Details' },
  },
  {
    path: 'profile',
    component: ProfileComponent,
    data: { title: 'My Profile' },
  },
  {
    path: 'departments',
    component: DepartmentsComponent,
    data: { title: 'Departments' },
  },
  {
    path: 'contact',
    component: ContactComponent,
    data: { title: 'Contact Us' },
  },
  {
    path: 'reservations',
    component: ReservationsComponent,
    data: { title: 'My Reservations' },
  },
  {
    path: 'department',
    component: DepartmentComponent,
    data: { title: 'Department' },
  },
  { path: 'reservation', component: ReservationComponent },
  {
    path: 'department/:id',
    component: DepartmentDetailComponent,
    data: { title: 'Department Details' },
  },
  {
    path: 'Settings',
    component: SettingsComponent,
    data: { title: 'Settings' },
  },
  {
  path: 'news',
  component: NewsComponent,
  data: { title: 'News' },
  },
  {
  path: 'news/:id',
  component: NewsDetailComponent,
  data: { title: 'News Details' },
  },
  {
    path: '**',
    component: NotfoundComponent,
    data: { title: '404' },
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
