# Avalonia QuadTree
Quadtree is a tree-based data structure that recursively partitions a two-dimensional space into four equal quadrants or regions. This structure is used to represent and store spatial data such as points, lines, and polygons in a more efficient way than using a simple list or array. A quadtree is a type of tree data structure where each node has at most four children. The root node represents the entire 2D space, which is divided into four quadrants. Each quadrant is represented by a child node of the root. The child nodes can also be further divided into four quadrants if they contain more than one point. This process continues recursively until all points are contained in individual leaves of the tree.

A quadtree is a way of organizing a two-dimensional space by breaking it down into smaller and smaller parts. It starts by dividing the space into four equal quadrants, and then it continues to subdivide each quadrant into four more quadrants, until all the subdivisions meet certain criteria.

To create a quadtree, we need to define the boundaries of the space we want to partition, and a function that determines how to split a space into four quadrants. We also need to decide how to store the data associated with each leaf node, and how to traverse the tree to access or modify the data. There are different types of quadtrees, such as region quadtrees, point quadtrees, line quadtrees, and curve quadtrees, depending on the type and shape of the data they represent.

<img src="demo/demo.gif" alt="Demo">

