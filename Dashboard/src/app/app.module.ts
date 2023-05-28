import { NgModule } from '@angular/core';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule} from '@angular/common/http';

import {
  PERFECT_SCROLLBAR_CONFIG,
  PerfectScrollbarConfigInterface,
  PerfectScrollbarModule,
} from 'ngx-perfect-scrollbar';

// Import routing module
import { AppRoutingModule } from './app-routing.module';

// Import app component
import { AppComponent } from './app.component';

// Import containers
import {
  DefaultHeaderComponent,
  DefaultLayoutComponent,
} from './containers';

import {
  AvatarModule,
  BadgeModule,
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  DropdownModule,
  FooterModule,
  FormModule,
  GridModule,
  HeaderModule,
  ListGroupModule,
  NavModule,
  ProgressModule,
  SharedModule,
  SidebarModule,
  TabsModule,
  UtilitiesModule,
} from '@coreui/angular';

import { IconModule, IconSetService } from '@coreui/icons-angular';
import { AllDoctorsComponent } from './views/doctors-views/all-doctors/all-doctors.component';
import { AddDoctorComponent } from './views/doctors-views/add-doctor/add-doctor.component';
import { EditDoctorComponent } from './views/doctors-views/edit-doctor/edit-doctor.component';
import { DetailsDoctorComponent } from './views/doctors-views/details-doctor/details-doctor.component';
import { AllPatientsComponent } from './views/patient-views/all-patients/all-patients.component';
import { AllDepartmentsComponent } from './views/department-views/all-departments/all-departments.component';
import { DetailsDepartmentComponent } from './views/department-views/details-department/details-department.component';
import { AddDepartmentComponent } from './views/department-views/add-department/add-department.component';
import { EditDepartmentComponent } from './views/department-views/edit-department/edit-department.component';
import { AllCampImagesComponent } from './views/campImage-views/all-camp-images/all-camp-images.component';
import { AddCampImageComponent } from './views/campImage-views/add-camp-image/add-camp-image.component';
import { AllReservationsComponent } from './views/reservations-views/all-reservations/all-reservations.component';
import { DetailsPatientComponent } from './views/patient-views/details-patient/details-patient.component';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true,
};

const APP_CONTAINERS = [
  DefaultHeaderComponent,
  DefaultLayoutComponent,
];

const DOCTORS_COMPONENTS = [
  AllDoctorsComponent,
  AddDoctorComponent,
  EditDoctorComponent,
  DetailsDoctorComponent
];

@NgModule({
  declarations: [AppComponent, ...APP_CONTAINERS, ...DOCTORS_COMPONENTS, AllPatientsComponent, AllDepartmentsComponent, DetailsDepartmentComponent, AddDepartmentComponent, EditDepartmentComponent, AllCampImagesComponent, AddCampImageComponent, AllReservationsComponent, DetailsPatientComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AvatarModule,
    FooterModule,
    DropdownModule,
    GridModule,
    HeaderModule,
    SidebarModule,
    IconModule,
    PerfectScrollbarModule,
    NavModule,
    ButtonModule,
    FormModule,
    UtilitiesModule,
    ButtonGroupModule,
    FormModule,
    ReactiveFormsModule,
    SidebarModule,
    SharedModule,
    TabsModule,
    ListGroupModule,
    ProgressModule,
    BadgeModule,
    ListGroupModule,
    CardModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy,
    },
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG,
    },
    IconSetService,
    Title
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
}
