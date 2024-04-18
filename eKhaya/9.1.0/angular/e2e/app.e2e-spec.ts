import { eKhayaTemplatePage } from './app.po';

describe('eKhaya App', function() {
  let page: eKhayaTemplatePage;

  beforeEach(() => {
    page = new eKhayaTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
