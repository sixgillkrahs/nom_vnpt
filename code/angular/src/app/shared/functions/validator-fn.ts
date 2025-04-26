import { AbstractControl, ValidatorFn } from '@angular/forms';

export class AppValidatorFn {
  public static oordiateValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      let coor = control.value;
      if (!coor) return null;
      else {
        var toado = coor.split(', ');
        if (
          toado.length == 2 &&
          isFinite(toado[0]) &&
          Math.abs(toado[0]) <= 90 && //valid Long
          isFinite(toado[1]) &&
          Math.abs(toado[1]) <= 180
        )
          return null
        else return { invalidCoordiate: true };
      }
    };
  }
  public static geoJsonValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const geoJson = control.value;

      // Check if the value is null or empty
      if (!geoJson || geoJson.trim() === '') {
        return null; // Return null if it's empty
      }

      try {
        // Attempt to parse the GeoJSON object
        const parsedJson = JSON.parse(geoJson);

        // Check if the parsed object is a valid GeoJSON object
        if (
          parsedJson &&
          parsedJson.type &&
          (parsedJson.type === 'Point' ||
            parsedJson.type === 'LineString' ||
            parsedJson.type === 'Polygon' ||
            parsedJson.type === 'MultiPoint' ||
            parsedJson.type === 'MultiLineString' ||
            parsedJson.type === 'MultiPolygon' ||
            parsedJson.type === 'GeometryCollection' ||
            parsedJson.type === 'Feature' ||
            parsedJson.type === 'FeatureCollection')
        ) {
          return null; // Return null if it's a valid GeoJSON object
        } else {
          return { invalidGeoJson: true }; // Return an error if it's an invalid GeoJSON object
        }
      } catch (error) {
        return { invalidGeoJson: true }; // Return an error if it fails to parse as JSON
      }
    };
  }
}
