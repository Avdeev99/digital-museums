import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { environment } from 'src/environments/environment.prod';
import { storage } from './core/auth/constants/api.constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'DigitalMuseums-UI';

  constructor(private translateService: TranslateService) {
    this.translateService.addLangs(environment.locales);
    this.translateService.setDefaultLang(environment.defaultLocale);

    const selectedLocale = localStorage.getItem(storage.selectedLocale);
    this.translateService.use(selectedLocale ?? environment.defaultLocale);
  }
}
