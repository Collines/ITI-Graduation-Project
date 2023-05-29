import { Component,OnInit } from '@angular/core';
import { AccountService } from 'src/app/Services/account.service';
import { Router } from '@angular/router';
import { UserEdit } from 'src/app/Interfaces/User/UserEdit';
import { Gender } from 'src/app/Enums/Gender'


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit  {
  langauge = localStorage.getItem('language');
  constructor(private accountService: AccountService, private router: Router){}
  Gender = Gender;
  userEdit: UserEdit = {
    gender: 1,
    ssn: '',
    firstName_EN: '',
    firstName_AR: '',
    lastName_EN: '',
    lastName_AR: '',
    email: '',
    phone: '',
    dob: new Date(),
    medicalHistory: null,
    password: null,
  };
  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        if (user) {
          this.accountService.getData(user.accessToken).subscribe({
            next: (udata) => {
              this.userEdit = udata;
              let date = new Date(udata.dob);
              this
                date.getFullYear() +
                '-' +
                (date.getMonth() + 1).toString().padStart(2, '0') +
                '-' +
                date.getDate().toString().padStart(2, '0');
            },
          });
        } else {
          this.router.navigate(['/login']);
        }
      },
    });
  }

  logout() {
    this.accountService.logout();
  }
}
