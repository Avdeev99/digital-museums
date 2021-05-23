import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { IOption } from 'src/app/core/form/form.interface';
import { UserService } from 'src/app/core/shared/services/user.service';
import { LinkingMuseumToUser } from '../../models/linking-museum-to-user.model';
import { MuseumService } from '../../services/museum.service';

@Component({
  selector: 'app-linking-museum-to-user',
  templateUrl: './linking-museum-to-user.component.html',
  styleUrls: ['./linking-museum-to-user.component.scss']
})
export class LinkingMuseumToUserComponent implements OnInit {
  public formGroup: FormGroup;

  public users$: Observable<Array<IOption>>;
  public museums$: Observable<Array<IOption>>;
  public isFetching: boolean = false;

  constructor(
    private fb: FormBuilder,
    private museumService: MuseumService,
    private userService: UserService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.initDropdowns();
  }

  public onSubmit(): void {
    if (this.formGroup.invalid) {
      return;
    }

    this.isFetching = true;

    const linkingMuseumToUser: LinkingMuseumToUser = this.formGroup.getRawValue();
    this.museumService.linkMuseumToUser(linkingMuseumToUser)
      .subscribe(() => {
        this.router.navigate(['/']);
        this.isFetching = false;
      });
  }

  private initForm(): void {
    this.formGroup = this.fb.group({
      museumId: new FormControl(null, [Validators.required]),
      userId: new FormControl(null, [Validators.required]),
    });
  }

  private initDropdowns(): void {
    this.museums$ = this.museumService.getBaseList();
    this.users$ = this.userService.getBaseList();
  }
}
