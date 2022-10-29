import { Component, OnInit } from '@angular/core';
import { navData } from './nav.data';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  constructor() { }
  
  navData = navData;

  ngOnInit(): void {
  }

}
