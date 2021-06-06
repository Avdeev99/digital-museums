import { Component, ElementRef, EventEmitter, Input, OnInit, Optional, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent implements OnInit {
  @Input()
  @Optional() 
  public control: FormControl;

  @Optional()
  @Input()
  public showUploadButton: boolean = false;

  @Optional()
  @Input()
  public allowMultipleFiles: boolean = true;

  @Optional()
  @Input()
  public selectFilesButtonType: string = 'button';

  @Optional()
  @Input()
  public selectButtonText: string = 'shared.controls.choose-files';

  @Optional()
  @Input()
  public acceptedTypes: string = '.png, .jpg, .jpeg';

  @Optional()
  @Input()
  public labelText: string = '';

  @Output()
  public selectedFilesChanged: EventEmitter<FileList> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
    this.control = !!this.control ? this.control : new FormControl(null);
  }

  onUploadClicked(event: any): void {

  }

  onSelectedFilesChanged(fileList: FileList): void {
    if (!!fileList && !!fileList.length) {
      this.control.setValue(fileList);
    }

    this.selectedFilesChanged.emit(fileList);
  }
}
