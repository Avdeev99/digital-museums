import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { storage } from 'src/app/core/auth/constants/api.constants';
import { IOption } from 'src/app/core/form/form.interface';

@Component({
  selector: 'app-select-language',
  templateUrl: './select-language.component.html',
  styleUrls: ['./select-language.component.scss']
})
export class SelectLanguageComponent implements OnInit {
  public languages: Array<IOption>;
  public defaultLang: string;

  constructor(public translateService: TranslateService) { }

  ngOnInit(): void {
    this.languages = this.translateService.getLangs().map(lang => {
      return {
        id: lang,
        name: lang,
      };
    });

    this.defaultLang = this.translateService.currentLang;
  }

  onLanguageChange(lang: string): void {
    localStorage.setItem(storage.selectedLocale, lang);
    this.translateService.use(lang);
  }
}
