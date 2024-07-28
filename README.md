If you have ever modded the .forge files of an Ubisoft game such as Ghost Recon: Breakpoint, you are probably aware of the issues that can arise due to duplicates.

Upon extracting a .forge or .data file using the Anvil Toolkit, its contents will be prefixed with numbers, e.g.: "1158_-_TP_Goggles_NVG-L3-GPNVG.BuildTable". Files provided by mod authors will often have different numbers, meaning if you paste them over and re-pack without checking for duplicates, you will likely experience issues in-game. Manually searching for pre-existing files and deleting them can be a long, tedious and error-prone process, especially if a mod has dozens of files. This tool aims to make your life a little bit easier.

* Place the .exe into the folder of the extracted .forge or .data file you wish to mod
* Run the program and choose an action to perform
* Once you have finished, be sure to remove the .exe from the folder before re-packing, otherwise Anvil will include it in the re-packing process!

## Find Duplicates

Find Duplicates will scan the extracted folder for any duplicate files and tell you their unprefixed names.

## Transfer Files

Provide the full directory path of files you wish to copy over to the extracted folder. The tool will copy them and remove the pre-existing ones for you!
