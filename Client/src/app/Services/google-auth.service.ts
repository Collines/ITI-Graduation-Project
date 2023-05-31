import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SocialAuthService, SocialUser } from "@abacritt/angularx-social-login";
import { GoogleLoginProvider } from "@abacritt/angularx-social-login";

@Injectable({
  providedIn: 'root'
})
export class GoogleAuthService {

  user: SocialUser | null;

  constructor(
    private http: HttpClient,
    private externalAuthService: SocialAuthService)
    {
      this.user = null;
	    this.externalAuthService.authState.subscribe((user: SocialUser) => {
	    console.log(user);
	    this.user = user;
	});
    }

  public signInWithGoogle = ()=> {
    this.externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  public signOutExternal = () => {
    this.externalAuthService.signOut();
  }
}
