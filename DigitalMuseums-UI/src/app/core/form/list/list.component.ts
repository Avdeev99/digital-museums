import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { EntityBase } from '../../shared/models/entity-base.model';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  @Input()
  public data: Array<EntityBase>

  @Output()
  public deleteEvent: EventEmitter<number> = new EventEmitter();

  @Output()
  public editEvent: EventEmitter<number> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  onDelete(id: number): void {
    this.deleteEvent.emit(id);
  }

  onEdit(id: number): void {
    this.editEvent.emit(id);
  }
}
