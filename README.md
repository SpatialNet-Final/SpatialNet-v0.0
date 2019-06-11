# SpatialNet-v0.0
### The knowledge graph (KG) that conceptualizes spatial qualities and maps the spatial signs to these concepts. 


![a conceptual represntation of SpatialNet](https://user-images.githubusercontent.com/47088273/58963174-fe2d0a00-87ac-11e9-8b7e-f677bca329a6.jpg "Sample concepts of SpatialNet")

#### The features of any urban constructs will be extracted using a specially devised neural network (NN) named SpatialFeaturesNet that utilizes state-of-the-art computer 3d vision convolutional techniques. SpatialNet will conceptualize these extracted spatial features using text mining technique. meaning that the SpatialFeaturesNet NN is used for extracting the features of any urban constructs, which are represented by 3d digital models or by sketches and Euclidian images, and the SpatialNet KG will be used then for conceptualizing the extracted features. The two neural networks will collectively represent Version 0.00 of the proposed scheme for conceptualizing urban constructs.
![the simulation model](https://user-images.githubusercontent.com/47088273/59296408-38ebe200-8c86-11e9-8d25-a92a50291f23.png "The proposed procedure for conceptualizing urban constructs, (1) is SpatialFeaturesNet and (2) is SpatialNet")

#### The code includes the _Theictionaryroj2013.exe_ file for running a specially devised tool for structuring the database needed for training a Deep Artificial Network for structuring SpatialNet, which is a knowledge graph (KG). _Theictionaryroj2013.exe_ can be found at (.\Theictionaryroj2013\Theictionaryroj2013\bin\Debug, the online path is  https://github.com/SpatialNet-Final/SpatialNet-v0.0/tree/master/Theictionaryroj2013/bin/Debug)

#### To use the .exe file and to structure  your own version of SpatialNet:
- you need to upload  seven specially formatted DHF5 file of seven corpora to  (_.\Theictionaryroj2013\Theictionaryroj2013\bin\Debug\ConceptNet_)
- The seven corpora are downloadable from (https://www.dropbox.com/sh/t9rjmejcmminwwj/AAB5TT3fLklKkbU3D5rJajgSa?dl=0)
- You need to be connected online to use the ConceptNet tab.
- to use the WordNet tab you need to upload the wordnet files at (_.\Theictionaryroj2013\Theictionaryroj2013\bin\Debug\wordnet_). A compressed tar file of the WordNet 3.1 database files is downloadable from https://wordnet.princeton.edu/download/current-version. you need to uncompress the files into the aforementioned _\Debug\wordnet_ path. 

the user interface of the computational tool that is prepared to be used for structuring the training dataset of the SpatialNet knowledge graph looks like the following:

![___the application UI](https://user-images.githubusercontent.com/47088273/59104925-1af34a00-8933-11e9-9544-19352dd2de3b.png "a graphical interface of the computational tool that will generate the training dataset")

![spatial language presentation__](https://user-images.githubusercontent.com/47088273/53517776-1f100000-3ad8-11e9-86a5-d8c08fe48140.gif)
