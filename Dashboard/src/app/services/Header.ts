import { IAdmin } from './../interfaces/IAdmin';
import { HttpHeaders } from '@angular/common/http';

export class Headers {

  private Header:HttpHeaders = new HttpHeaders()
  .set('content-type', 'application/json')
  .set('Access-Control-Allow-Origin', '*')
  

  getHeaders() {
    const temp = localStorage.getItem("admin");
    if (temp)
    {
      const admin: IAdmin = JSON.parse(temp);
      this.Header = this.Header.set('Authorization',`Bearer ${admin.accessToken}`);
    }
    return this.Header;
  }
}
