import os
import pyperclip

messageTypes = []
blacklistedMessageTypes = ["MessageTypes.Actionlib.ActionStatus"]

pathToMessageTypes = r"C:\Users\poole\OneDrive\Documents\GitHub Repositories\UndergradHonorsThesis\ros-sharp-master\Libraries\RosBridgeClient\MessageTypes"
pathsToCheck = [pathToMessageTypes]

while len(pathsToCheck)>0:
    pathToCheck = pathsToCheck.pop(0)
    if(os.path.isdir(pathToCheck)):
        subFiles = os.listdir(pathToCheck)
        for subFile in subFiles:
            pathsToCheck.append(os.path.join(pathToCheck,subFile))
    else:
        splitArray = pathToCheck.split("\\")
        message1 = "MessageTypes"
        message2 = splitArray[len(splitArray)-3]
        message3 = splitArray[len(splitArray)-1]
        message3 = message3[0:(len(message3)-3)]
        messageType = message1 + "." + message2 + "." + message3
        if messageType not in blacklistedMessageTypes:
            messageTypes.append(messageType)
            #print(messageType,"not blacklisted.")

#Format printed function
varNameType = "type"
varNameTopicName = "TopicName"
varNameRosSocket = "rosSocket"
varNamePubID = "publisherID"
printedFunction = ""

for i in range(0,len(messageTypes)):
    if i != 0:
        printedFunction += "else "
    printedFunction += "if(" + varNameType + " == typeof(" + messageTypes[i] + ")){\n\t"
    printedFunction += varNamePubID + " = " + varNameRosSocket + ".Advertise<" + messageTypes[i] + ">(" + varNameTopicName + ");\n"
    printedFunction += "}"

print(printedFunction)
pyperclip.copy(printedFunction)