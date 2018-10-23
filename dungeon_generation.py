import random

rooms = input('Size of dungeon > ' )                     # Number of rooms to generate
CHILD_START_DIST = [[0.0, 0.3, 0.35, 0.45], [0.0, 0.1, 0.25, 0.25, 0.25, 0.15], [], [0.0, 0.0, 0.20, 0.25, 0.25, 0.15, 0.1, 0.05]]
CHILD_END_DIST = [[0.9, 0.1, 0.0, 0.0], [0.85, 0.1, 0.05, 0.0, 0.0, 0.0], [], [0.80, 0.1, 0.05, 0.05, 0.0, 0.0, 0.0, 0.0]]
SIZE_START_DIST = [0.65, 0.25, 0.1]
SIZE_MID_DIST = [0.2, 0.35, 0.45]
SIZE_END_DIST = [1.0, 0, 0]


class ChildrenDistribution:
    def __init__(self, roomLimit, startDist, endDist):
        self.dungeonSize = roomLimit
        self.beginDistributions = startDist
        self.endDistributions = endDist

    def combineDistributions(self, currentRoom, roomSize):
        endRatio = currentRoom / self.dungeonSize
        startRatio = 1 - endRatio
        combinedDist = []
        startDist = self.beginDistributions[roomSize - 1]
        endDist = self.endDistributions[roomSize - 1]

        # Combine distributions
        for i in range(len(startDist)):
            combinedDist.append(startRatio * startDist[i] + endRatio * endDist[i])

        # Normalize
        total = sum(combinedDist)
        normalizedDist = [x / total for x in combinedDist]

        return normalizedDist

    # Returns number from distribution
    def getValue(self, currentRoom, roomSize):
        combinedDist = self.combineDistributions(currentRoom, roomSize)
        # First room can have 1-4
        if currentRoom == 1:
            randomNumber = random.randrange(1, 5)
            return randomNumber
        else: 
            randomNumber = random.random()
            children = -1
            while randomNumber > 0:
                randomNumber -= combinedDist[children + 1]
                children +=1
        return children


class ShapeDistribution:
    def __init__(self, roomLimit, startDist, midDist, endDist):
        self.dungeonSize = roomLimit
        self.startDist = startDist
        self.midDist = midDist
        self.endDist = endDist

    def combineDistributions(self, currentRoom):
        endRatio = currentRoom / self.dungeonSize * 3
        midRatio = max(0, ((-1 / self.dungeonSize)*(currentRoom - self.dungeonSize/2)**2 + 0.5)) # -1/dungeonSize(x-dungeonSize/2)^2 + 0.5
        startRatio = 0.333 - endRatio
        combinedDist = []
        startDist = self.startDist
        midDist = self.midDist
        endDist = self.endDist

        # Combine distributions
        for i in range(len(startDist)):
            combinedDist.append(startRatio * startDist[i] + midRatio * midDist [i] + endRatio * endDist[i])

        # Normalize
        total = sum(combinedDist)
        normalizedDist = [x / total for x in combinedDist]

        return normalizedDist

    # Returns number from distribution: size, orientation
    def getValue(self, currentRoom):
        combinedDist = self.combineDistributions(currentRoom)
        randomNumber = random.random()
        size = 0
        while randomNumber > 0:
            randomNumber -= combinedDist[size]
            size +=1
        if size == 0:
            return 1, 0
        elif size == 2:
            return size, self.chooseOrientation()
        return size, 0

    # Chooses orientation for 1x2 or 2x1 rooms
    def chooseOrientation(self):
        randomNumber = random.random()
        if randomNumber > 0.5:
            return 0
        else:
            return 1


class Node:
    def __init__(self, width, height, x, y):
        self.height = height
        self.width = width
        self.position = (x, y)
        self.updateEntrances()

    def updateEntrances(self):
        height = self.height
        width = self.width
        x = self.position[0]
        y = self.position[1]

        if height == 1 and width == 1:
            self.entrances = [(x, y+1), (x, y-1), (x+1, y), (x-1, y)]
        elif height == 2 and width == 1:
            self.entrances = [(x, y+1), (x, y-2), (x+1, y), (x+1, y-1), (x-1, y), (x-1, y-1)]
        elif height == 1 and width == 2:
            self.entrances = [(x, y+1), (x+1, y+1), (x+2, y), (x+1, y-1), (x, y-1), (x-1, y)]
        else:
            self.entrances = [(x, y+1), (x+1, y+1), (x+2, y), (x+2, y-1), (x+1, y-2), (x, y-2), (x-1, y-1), (x-1, y)]

    def __str__(self):
        return str("(" + str(self.position[0]) + ", " + str(self.position[1]) + ")")

    __repr__ = __str__

class Matrix:
    # I'm switching the y and x axes so that it's easier to print out the dungeon in console (using horizontal as nested array as opposed to vertical)
    def __init__(self, roomLimit):
        self.size = roomLimit
        self.adjacencyList = {}

        self.matrix = []
        for i in range((roomLimit * 2) + 1):
            horizontal = [None] * ((roomLimit * 2) + 1)
            self.matrix.append(horizontal)

    def getTile(self, x, y):
        x += self.size
        y += self.size
        y = abs(y - (self.size * 2 + 1))
        #print(x, y, len(self.matrix))
        if (y >= len(self.matrix)) or (x >= len(self.matrix)):
            #print("NONE")
            return None
        return self.matrix[y][x]

    def setTile(self, x, y, node):
        x += self.size
        y += self.size
        y = abs(y - (self.size * 2 + 1))
        self.matrix[y][x] = node

    def connectNodes(self, firstNode, secondNode):
        if firstNode in self.adjacencyList:
            self.adjacencyList[firstNode.position].append(secondNode.position)
        else:
            self.adjacencyList[firstNode.position] = [secondNode.position]

        if secondNode in self.adjacencyList:
            self.adjacencyList[secondNode.position].append(firstNode.position)
        else:
            self.adjacencyList[secondNode.position] = [firstNode.position]

    def isConnected(self, firstNode, secondNode):
        if not firstNode or not secondNode:
            return False
        return secondNode.position in self.adjacencyList[firstNode.position]

    def choosePosition(self, node):
        nodePosition = node.position
        possiblePositions = []

        # Create possible positions to add a new node. This only needs to happen with greater than 1x1 rooms
        for x in range(node.width):
            for y in range(node.height):
                possiblePositions.append((nodePosition[0] - x, nodePosition[1] + y))

        while possiblePositions:
            positionToTry = random.choice(possiblePositions)
            possiblePositions.remove(positionToTry)
            if self.checkPosition(positionToTry, node):
                return positionToTry
        return False

    # if for every point in the proposed position there is None, it is valid
    def checkPosition(self, proposedPosition, node):
        for x in range(node.width):
            for y in range(node.height):
                if self.getTile(proposedPosition[0] + x, proposedPosition[1] - y):
                    return False
        return True

    def setNode(self, node):              
        for x in range(node.width):
            for y in range(node.height):
                self.setTile(node.position[0] +  x, node.position[1] - y, node)

    def printMatrix(self):
        for row in self.matrix:
            print(row)


def generate(size=5):
    # Initialize the data structs
    roomLimit = size
    currentRoom = 1
    nodes = []
    toGenerateChildren = []
    matrix = Matrix(roomLimit)
    childrenDistribution = ChildrenDistribution(roomLimit, CHILD_START_DIST, CHILD_END_DIST)
    shapeDistribution = ShapeDistribution(roomLimit, SIZE_START_DIST, SIZE_MID_DIST, SIZE_END_DIST)

    # Create startnode
    startNode = Node(1, 1, 0, 0)
    nodes.append(startNode)
    toGenerateChildren.append(startNode)
    matrix.setTile(0, 0, startNode)

    # Start generation loop
    while toGenerateChildren:
        node = toGenerateChildren.pop(0)
        print("Generating children for node: " + "(" + str(node.position[0]) + ", " + str(node.position[1]) + ")")
        # if room limit reached
        if len(nodes) >= roomLimit:
            print("Room limit reached: " + str(len(nodes)) + " vs. " + str(roomLimit))
            break

        # Get number of children
        childrenCount = 0
        childrenCount = childrenDistribution.getValue(currentRoom, node.height*node.width)
        childrenCount = min(roomLimit - len(nodes), childrenCount)

        # Generate Children -> choose size/orientation + position. If not possible, delete.
        for c in range(childrenCount):
            if len(nodes) >= roomLimit:
                break

            # choose entrance position to new child node
            if node.entrances:
                entrance = random.choice(node.entrances)
                node.entrances.remove(entrance)
                print("Entrance found at: " + "(" + str(entrance[0]) + ", " + str(entrance[1]) + ")")
            # if no possible entrances, break loop
            else:
                break

            # TODO -> edge case where all children are on top of pre-existing rooms -> create new child
            # check matrix for pre-existing room. If so connect rooms with probability
            tile = matrix.getTile(entrance[0], entrance[1])
            if tile:
                print("Pre-existing room found... connecting old node^")
                matrix.connectNodes(tile, node)
                continue

            # else, choose size and instantiate new node
            # If all possible orientations of new child overlaps existing nodes, then don't instantiate a smaller one. This is to bias sparse dungeons
            # continue
            size, orientation = shapeDistribution.getValue(currentRoom)
            if size == 1:
                newChild = Node(1, 1, entrance[0], entrance[1])
                position = (entrance[0], entrance[1])
            elif size == 2:
                if orientation ==  0:
                    newChild = Node(1, 2, entrance[0], entrance[1])
                    position = matrix.choosePosition(newChild)
                else:
                    newChild = Node(2, 1, entrance[0], entrance[1])
                    position = matrix.choosePosition(newChild)
            else:
                newChild = Node(2, 2, entrance[0], entrance[1])
                position = matrix.choosePosition(newChild)
            print("new node of size " + str(size) + " and orientation " + str(orientation) + " generated at: " + "(" + str(position[0]) + ", " + str(position[1]) + ")")
            # if a position has been returned (false indicates no valid positions) set the node into the matrix
            if position:
                # update the node with the new position (as opposed to entrance position)
                print("position valid")
                newChild.position = position
                newChild.updateEntrances()
                nodes.append(newChild)
                toGenerateChildren.append(newChild)
                matrix.setNode(newChild)
                matrix.connectNodes(node, newChild)
            else:
                print("position invalid")

            # if to generate is empty but size hasn't yet been reached, 
            # choose a random existing node to generate more chilren from.
            if not toGenerateChildren and len(nodes) < roomLimit:
                toGenerateChildren.append(random.choice(nodes))

    return matrix

matrix = generate(int(rooms))
matrix.printMatrix()