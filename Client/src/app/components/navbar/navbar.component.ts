import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  constructor(private accountService: AccountService) {}
  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        this.isLogged = !!user;
        if (user) this.Username = user.patient.fullName;
      },
    });
  }
  Username = '';
  dropdownShow=false;
  dropDownClick(){
    this.dropdownShow=!this.dropdownShow;
  }
  isLogged: boolean = false;
  Logout() {
    this.accountService.logout();
    this.isLogged = false;
  }
}
