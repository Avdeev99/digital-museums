import { Component, EventEmitter, Input, OnInit, Optional, Output, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from '../../models/menu-item.model';

@Component({
  selector: 'app-submenu',
  templateUrl: './submenu.component.html',
  styleUrls: ['./submenu.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class SubmenuComponent implements OnInit {
  @Input() 
  public menuList: Array<MenuItem> = [];

  @Input()
  @Optional() 
  public title: string = "Menu";

  @Input()
  @Optional()
  public matIconType: string = "menu";

  @Output()
  public menuItemSelected: EventEmitter<any> = new EventEmitter();

  constructor(
    private router: Router
  ) {}

  ngOnInit(): void {
  }

  public onMenuItemSelected($event: Event, href: string): void {
    if (href && href !== '#') {
      this.router.navigate([href]);
    }

    this.menuItemSelected.emit($event);
  }
}
