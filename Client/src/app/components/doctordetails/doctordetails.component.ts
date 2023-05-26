import { Component, OnInit } from '@angular/core';
import { DoctorsService } from 'src/app/Services/doctors.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-doctordetailss',
  templateUrl: './doctordetails.component.html',
  styleUrls: ['./doctordetails.component.css'],
})
export class DoctordetailsComponent implements OnInit {
  ID: any;
  Doctor: any;
  constructor(
    private DoctorsService: DoctorsService,
    private Route: ActivatedRoute,
    private router: Router,
    private myAccountService: AccountService
  ) {
    this.ID = this.Route.snapshot.params['id'];
    myAccountService.currentUser$.subscribe({
      next: (user) => {
        if (user) {
          this.DoctorsService.GetById(this.ID, user.accessToken).subscribe({
            next: (data) => {
              this.Doctor = data;
              console.log(data);
            },
            error: (err) => console.log(err),
          });
        } else {
          router.navigate(['/login']);
        }
      },
      error: (e) => {
        router.navigate(['/login']);
      },
    });
  }
  ngOnInit() {}
}
