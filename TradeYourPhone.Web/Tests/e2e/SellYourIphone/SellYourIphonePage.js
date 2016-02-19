var SellYourIphonePage = function () {

    this.heading = element(by.css('h1'));
    this.subHeading = element(by.css('h2'));
    this.headingImage = element(by.css('.banner-iphone > div > img'));

    this.instructionHeading = element(by.css('h3'));
    this.instructionText = element(by.css('.instruction-text'));

    this.phoneRows = element.all(by.repeater('phone in phoneModels'));

    // FUNCTION NAME: countRowTiles
    // PARAMETERS: rowNo: int - row index you want to count the tiles of
    // DESCRIPTION: Counts the number of phone-tile classes there are in a phone row
    this.countRowTiles = function (rowNo) {
        var row = this.phoneRows.get(rowNo);
        var tiles = row.all(by.css('.phone-tile'));
        return tiles.count();
    }

    // FUNCTION NAME: getRowTile
    // PARAMETERS: rowNo: int - row index you want get the tile from
    //             tileNo: int - tile index you want to get
    // DESCRIPTION: Selects the tile from the row
    this.getRowTile = function (rowNo, tileNo) {
        var row = this.phoneRows.get(rowNo);
        return row.get(tileNo);
    }
};
module.exports = SellYourIphonePage;
