import torch
from  torch.utils.data import  Dataset
import torchvision.transforms as transforms
import csv
class MotionDataset(Dataset):
    
    def __init__(self, csv_files=["../../camera_pose.csv"], samplelength=100, transform=None):
        self.samplelength=samplelength
        files=[]
        for csv_file in csv_files:
            file = open(csv_file)
            file = csv.reader(file,delimiter=',')
            data=[]
            for row in file:
                if len(row)<3: continue
                rowdata=[float(row[i]) for i in range(3)]
                data.append(rowdata)
            files.append(data)
        self.data = []
        
        data = torch.tensor(files)
        self.data = data.transpose(0,1).reshape((-1,3*len(csv_files)))
        self.size = self.data.size(0)-self.samplelength
        return
    def __len__(self):
        # return len(self.)
        return self.size

    def __getitem__(self, idx):
        return self.data[idx:idx+self.samplelength,:]
        # if self.transform is not None:
        #     item = self.transform(item)
        
        # return self.data
if __name__=="__main__":
    dataset = MotionDataset(csv_files=["../../../camera_pose.csv","../../../camera_pose.csv"])
    print(len(dataset))
    for i in range(len(dataset)):
        print(dataset[i].size(),i)
        
    pass
