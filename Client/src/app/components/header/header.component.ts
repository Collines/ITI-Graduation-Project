import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import {BannerService} from 'src/app/Services/banner.service'



@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  showSlider: boolean = true;
  constructor(private router: Router,private bannerService:BannerService) {
    this.router.events.subscribe((v) => {
      if (v instanceof NavigationEnd) {
        if (v.url == '/login' || v.url == '/register' || v.url == '/dashboard')
          this.showSlider = false;
      }
    });
  }
  Banners: any;
  img1:string="";

  ngOnInit(): void {
    this.bannerService.GetAllBanners().subscribe({
      next: (data) => {
        this.Banners =data;
        this.img1=this.Banners[0].imagePath;
        this.img1=this.img1.replace(/\\/g, "\\\\");
        console.log(this.img1);
      },
      error: (err) => console.log(err),
    });
  }
  
}
