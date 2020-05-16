import { browser, by, element } from 'protractor';

export class AppPage {
  navigateTo(path: string) {
    return browser.get(path);
  }

  getTitleText() {
    return element(by.css('h1')).getText();
  }
}
