import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/Services/account.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  TextDir: string;

  constructor(
    private accountService: AccountService,
    public translate: TranslateService
  ) {
    translate.addLangs(['en', 'ar']);
    let langauge = localStorage.getItem('language');
    if (langauge) {
      this.translate.setDefaultLang(langauge);
      this.translate.currentLang = langauge;
      this.TextDir = langauge == 'en' ? 'ltr' : 'rtl';
    } else {
      this.translate.setDefaultLang('en');
      this.TextDir = 'ltr';
    }
    document.dir = this.TextDir;
  }

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        this.isLogged = !!user;
        if (user) this.Username = user.fullName;
      },
    });
  }

  Username = '';
  dropdownShow = false;

  dropDownClick() {
    this.dropdownShow = !this.dropdownShow;
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
    location.reload();
    document.dir = lang === 'en' ? 'ltr' : 'rtl';
  }
}
