import { AccountService } from "src/app/services/account.service";
import { Component, Input } from "@angular/core";

import { ClassToggleService, HeaderComponent } from "@coreui/angular";

@Component({
  selector: "app-default-header",
  templateUrl: "./default-header.component.html",
})
export class DefaultHeaderComponent extends HeaderComponent {
  @Input() sidebarId: string = "sidebar";

  constructor(
    private classToggler: ClassToggleService,
    private AccountService: AccountService
  ) {
    super();
  }

  Logout() {
    this.AccountService.logout();
  }
}
