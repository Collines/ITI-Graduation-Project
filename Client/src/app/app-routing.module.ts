import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientComponent } from './components/client/client.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AppComponent } from './app.component';

const routes: Routes = [
  {path:"",component:ClientComponent},
  {path:"Home",component:ClientComponent},
  {path:"Dashboard",component:DashboardComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
