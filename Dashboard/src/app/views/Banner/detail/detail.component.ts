import { BannerService } from 'src/app/services/banner.service';
import { Router, ActivatedRoute} from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {

  constructor(
    private BannersService:BannerService,
    private Route:ActivatedRoute,
    private Router:Router,
    private AccountService:AccountService)
    {
      this.ID = this.Route.snapshot.params["id"];
    }
  ngOnInit(): void {
    let admin = this.AccountService.getAdmin()
    if(!admin) {
      this.Router.navigate(["/login"]);
    }

    this.BannersService.GetById(this.ID).subscribe({
      next: data => {this.Banner = data;
      console.log(data)},
      error: err => console.log(err)
    })
  }
  ID:any;
  Banner:any;

  DeleteBanner(value:number){
    if(confirm(`Do You Want To Delete Banner No. ${value}`))
    {
      this.BannersService.DeleteBanner(value).subscribe({
        next: () => {alert(`Banner No.${value} Has Been Deleted`);
                      this.Router.navigate(['/Banners'])},
        error: err => console.log(err)
      })
    }
  }


}
