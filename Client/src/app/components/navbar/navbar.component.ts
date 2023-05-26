import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/Services/account.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {

  Lang:(string|null);
  TextDir:string;

  constructor(private accountService: AccountService, public translate: TranslateService) {

    translate.addLangs(['en', 'ar']);
    translate.setDefaultLang("en");

    this.Lang = sessionStorage.getItem("Lang");
    if(this.Lang)
      translate.use(this.Lang);

    if(this.Lang === "ar")
      this.TextDir = 'rtl';
    else
      this.TextDir = 'ltr';

    document.dir = this.TextDir;
  }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        this.isLogged = !!user;
        if (user) this.Username = user.fullName;
      },
    });
    let langauge = localStorage.getItem('language');
    if (langauge) {
      this.translate.setDefaultLang(langauge);
      this.translate.currentLang = langauge;
    } else {
      this.translate.setDefaultLang('en');
    }
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

  // translation switcher
  switchLang(lang: string) {
    localStorage.setItem('language', lang);
    this.translate.use(lang);
    sessionStorage.setItem("Lang",lang)

    if(lang === "ar"){
      this.TextDir = 'rtl';
      sessionStorage.setItem("Dir",'rtl')
    }
    else {
      this.TextDir = 'ltr';
      sessionStorage.setItem("Dir",'ltr')
    }

    document.dir = this.TextDir
  }
}
