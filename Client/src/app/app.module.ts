import { NgModule } from '@angular/core';
import { BrowserModule, Title } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AboutComponent } from './components/about/about.component';
import { DoctorComponent } from './components/doctor/doctor.component';
import { DoctordetailsComponent } from './components/doctordetails/doctordetails.component';
import { ProfileComponent } from './components/profile/profile.component';
import { DepartmentsComponent } from './components/departments/departments.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ContactComponent } from './components/contact/contact.component';
import { ReservationsComponent } from './components/reservations/reservations.component';
import { DepartmentComponent } from './components/department/department.component';

//For Translation
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
//

import { ReservationComponent } from './components/reservation/reservation.component';
import { DepartmentDetailComponent } from './components/department-detail/department-detail.component';
import { NotfoundComponent } from './components/notfound/notfound.component';
import { SettingsComponent } from './components/settings/settings.component';

//For Google SignIn
import { SocialLoginModule, SocialAuthServiceConfig } from '@abacritt/angularx-social-login';
import { GoogleLoginProvider } from '@abacritt/angularx-social-login';
import { HomeNewsComponent } from './components/home-news/home-news.component';
import { NewsComponent } from './components/news/news.component';
import { NewsItemComponent } from './components/news-item/news-item.component';
import { NewsDetailComponent } from './components/news-detail/news-detail.component';
import { NewsAsideComponent } from './components/news-aside/news-aside.component';
//

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    AboutComponent,
    DoctorComponent,
    DoctordetailsComponent,
    ProfileComponent,
    DepartmentsComponent,
    ContactComponent,
    ReservationsComponent,
    ReservationComponent,
    DepartmentComponent,
    DepartmentDetailComponent,
    NotfoundComponent,
    SettingsComponent,
    HomeNewsComponent,
    NewsComponent,
    NewsItemComponent,
    NewsDetailComponent,
    NewsAsideComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: httpTranslateLoader,
        deps: [HttpClient],
      },
    }),
    SocialLoginModule
  ],
  providers: [
    Title,
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '551365345955-tl754n7j47hofn2au1t8ouchrpon9fsq.apps.googleusercontent.com'
            )
          },
        ],
        onError: (err) => {
          console.error(err);
        }
      } as SocialAuthServiceConfig
    }
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
