import { AccountService } from 'src/app/Services/account.service';
import { BannerService } from 'src/app/services/banner.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-all-banner',
  templateUrl: './all-banner.component.html',
  styleUrls: ['./all-banner.component.scss']
})
export class AllBannerComponent implements OnInit {

  constructor(
    private Bannerserv:BannerService,
    private Router:Router,
    private AccountService:AccountService){}

  Banners:any;
  NoData:boolean = false;
  ErrorMessage:any;

  ngOnInit(): void {
    let admin = this.AccountService.getAdmin()
    if(!admin) {
      this.Router.navigate(["/login"]);
    }

    this.Bannerserv.GetAllBanners().subscribe({
      next: data => {this.Banners = data;
        this.NoData = false;
        if(!this.Banners.length){
          this.NoData = true;
          this.ErrorMessage = "No Banners Found"
        }
      },
      error: err => {
        this.NoData = true;
        this.ErrorMessage = "Can't Connect to Server"
      }
    })
  }


  DeleteBanner(value:number){
    if(confirm(`Do You Want To Delete Banner No. ${value}`))
    {
      this.Bannerserv.DeleteBanner(value).subscribe({
        next: () => this.Banners =  this.RemoveObjectWithId(this.Banners,value),
        error: err => console.log(err)
      })
    }
  }

  RemoveObjectWithId(arr:any, id:number) {
    const objWithIdIndex = arr.findIndex((obj:any) => obj.id == id);

    if (objWithIdIndex > -1) {
      arr.splice(objWithIdIndex, 1);
    }
    return arr;
  }
}
