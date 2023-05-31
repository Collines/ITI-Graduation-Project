import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CampImageService } from 'src/app/services/camp-image.service';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-all-camp-images',
  templateUrl: './all-camp-images.component.html',
  styleUrls: ['./all-camp-images.component.scss']
})
export class AllCampImagesComponent implements OnInit {

  constructor(
    private ImagesService:CampImageService,private Router:Router,
    private AccountService:AccountService){}
  Images:any;

  ngOnInit(): void {
    let admin = this.AccountService.getAdmin()
    if(!admin) {
      this.Router.navigate(["/login"]);
    }
  
  this.ImagesService.GetAllCampImages().subscribe({
    next: data => {this.Images = data;
  },
    error: err => console.log(err)
  })
  }
  
  DeleteImage(value:number){
    if(confirm(`Do You Want To Delete Doctor No. ${value}`))
    {
      this.ImagesService.DeleteCampImage(value).subscribe({
        next: () => this.Images =  this.RemoveObjectWithId(this.Images,value),
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