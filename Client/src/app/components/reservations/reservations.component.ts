import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Reservation } from 'src/app/Interfaces/reservation';
import { AccountService } from 'src/app/Services/account.service';
import { ReservationService } from 'src/app/Services/reservation.service';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css'],
})
export class ReservationsComponent implements OnInit {
  reservations: Reservation[] = [];
  constructor(
    private reservationService: ReservationService,
    private accountService: AccountService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        if (user) {
          this.reservationService
            .GetAllReservations(user.id, user.accessToken)
            .subscribe({
              next: (data) => {
                this.reservations = data;
              },
            });
        } else this.router.navigate(['/login']);
      },
      error: (err) => {
        this.router.navigate(['/login']);
      },
    });
  }
  logout() {
    this.accountService.logout();
  }
}
