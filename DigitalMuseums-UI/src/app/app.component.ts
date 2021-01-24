import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { environment } from 'src/environments/environment.prod';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'DigitalMuseums-UI';

  constructor(private translateService: TranslateService) {
    this.translateService.setDefaultLang(environment.defaultLocale);
    this.translateService.use(environment.defaultLocale);
  }
}
