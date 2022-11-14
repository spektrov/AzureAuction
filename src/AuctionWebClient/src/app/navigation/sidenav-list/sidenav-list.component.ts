import {Component, OnInit, Output, EventEmitter, Input} from '@angular/core';
@Component({
  selector: 'app-sidenav-list',
  templateUrl: './sidenav-list.component.html',
  styleUrls: ['./sidenav-list.component.css']
})
export class SidenavListComponent implements OnInit {
  @Input() isLoggedIn? : Boolean;

  @Output() sidenavClose = new EventEmitter();

  constructor() {
  }

  ngOnInit() {
  }

  public onSidenavClose = () => {
    this.sidenavClose.emit();
  }
}
