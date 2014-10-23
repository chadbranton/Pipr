##################################################################
# PortScan                                                       #
# Author:  Chad Branton                                          #
# Purpose: Scan ports of an imported list of IP addresses.       #
##################################################################

import sys
sys.path.append('C:\Python33\Lib')
import csv
import socket
from datetime import datetime
from concurrent.futures import ThreadPoolExecutor



##################################################################
#  Create a filename to store the output                         #
##################################################################

scanFile = open('test10', 'w')

##################################################################
#  User input to accept the number of threads, range of ports,   #
#  and the connection timeout.                                   #
##################################################################
#THREADS = int(input("how many threads? "))
#PORT_START = int(input("What port would you like me to start with? "))
#PORT_END = int(input("What port would you like to to end with? "))
#CONNECTION_TIMEOUT = 1

##################################################################
#  importIP():                                                   #
#  Imports a user selected csv file of IP's and builds a string  #
#  list of IPs                                                   #   
#  Return:  List of IP addresses                                 #
##################################################################
def importIP():
    #i = input("What is the filename that you want me to import? ")
    file = open("C:\\Users\\Sarah Frost\\OneDrive\\PortScan\\PortScan\\portScanIPs.csv");
    try:
        reader = csv.reader(file)
        ports = [row[0] for row in reader]
    except:
        pass
    return ports

##################################################################
#  ping():                                                       #
#  Ping each port for each IP address and returns True if port   #
#  is open and False if not.                                     #
##################################################################
def ping(host, port, results = None):
    try:
        socket.socket().connect((host, port))
        if results is not None:
            results.append(port)
        print ("\t** OPEN PORT ** " + str(port))
        scanFile.write("\t** OPEN PORT ** " + str(port) + "\n")
        return True
    except:
        print("\nTesting Port " + str(port))
        scanFile.write("Testing Port " + str(port) + "\n")
        return False

##################################################################
#  scanPorts()
#  Scans ports and creates a list of ports that are open         #
##################################################################
def scanPorts(host):
    time1 = datetime.now()
    openPorts = [] #list of ports that are open
    socket.setdefaulttimeout(CONNECTION_TIMEOUT)
    with ThreadPoolExecutor(max_workers = THREADS) as executor:
        print(">>>>>>Testing Host " + host + " <<<<<<\n")
        scanFile.write(">>>>>>>Testing Host " + host + " <<<<<<<\n")
        for port in range(1, 1000):
            executor.submit(ping, host, port, openPorts)
    print("\nDone.")
    
    openPorts.sort()
    print(str(len(openPorts)) + " port(s) that are open.")
    scanFile.write(str(len(openPorts)) + " port(s) that are open.\n")
    print(openPorts)
    scanFile.write(str(openPorts) + "\n")
    time2 = datetime.now()
    actualTime = time2 - time1
    scanFile.write('The test took ' + str(actualTime) + ' seconds \n')

##################################################################
#  main()                                                        #
##################################################################
def main():     
    list = importIP()    
    for port in list:
         print('--------------------------------------------------------\n')
         print('Threads: ', THREADS)
         print('Start Port', PORT_START)
         print('End Port', PORT_END)

         scanFile.write('--------------------------------------------------------\n')
         scanFile.write('Threads: ' + str(THREADS) + "\n")
         scanFile.write('Start Port: ' + str(PORT_START) + "\n")
         scanFile.write('End Port: ' + str(PORT_END) + "\n\n")
         
         scanPorts(port)

##################################################################
# Execute!!!                                                     #
##################################################################
if __name__ == "__main__":
    main()