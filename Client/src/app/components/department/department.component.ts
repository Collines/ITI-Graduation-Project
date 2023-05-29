import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Department } from 'src/app/Interfaces/Department';
import { Doctor } from 'src/app/Interfaces/Doctor';
import { User } from 'src/app/Interfaces/User/user';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css'],
})
export class DepartmentComponent {
  constructor(
    private Route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService
  ) {
    this.department.id = Route.snapshot.params['id'];
    accountService.currentUser$.subscribe({
      next: (user) => {
        if (user) this.user = user;
      },
    });
  }

  @Input() department: Department = {
    id: 0,
    title: '',
    description: '',
    numberOfBeds: 0,
  };
  doctors: Doctor[] = [];
  user: User = {
    id: 0,
    fullName: '',
    accessToken: '',
    refreshToken: '',
    expiration: 0,
  };
}
