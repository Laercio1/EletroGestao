/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/no-unused-vars */
import { Component, OnDestroy, OnInit } from '@angular/core'
import { Router } from '@angular/router'

@Component({
  selector: 'app-public',
  templateUrl: './public.component.html',
})
export class PublicComponent implements OnInit, OnDestroy {

  test: Date = new Date()
  public isCollapsed = true

  constructor (private router: Router) { }

  ngOnInit () {
    const html = document.getElementsByTagName('html')[0]
    html.classList.add('auth-layout')
    const body = document.getElementsByTagName('body')[0]
    body.classList.add('bg-default')
    this.router.events.subscribe((event: any) => {
      this.isCollapsed = true
    })

  }

  ngOnDestroy () {
    const html = document.getElementsByTagName('html')[0]
    html.classList.remove('auth-layout')
    const body = document.getElementsByTagName('body')[0]
    body.classList.remove('bg-default')
  }
}
