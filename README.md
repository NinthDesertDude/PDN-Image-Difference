# Image Difference
This is a plugin for Paint.NET that stores a copy of one image and compares it to a later copy. Comparison is done by removing all the pixels that match or don't match. The background is made transparent for all unaffected pixels.


## Usage
**Store Unedited Image**: Stores the image. This must be done before you can find the difference to the current image.

**Invert Isolated Area**: The changed pixels are kept the same and the unchanged pixels are made transparent instead.

**Invert Resulting Picture**: The unchanged pixels are made transparent and the changed pixels are replaced with the original pixels.

**Find Difference to Current Image**: Compares the current image to the stored image. The original must be stored first. If there are no invert options selected, all changes in the current image not present in the original will be kept and all other pixels will be made transparent.
