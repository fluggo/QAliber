<?xml version="1.0" encoding="iso-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <!-- DOCUMENT TEMPLATE -->
  <!-- Format the whole document as a valid HTML document -->
  <xsl:template match="/">
    <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
        <title>QAliber Help</title>
        <style type="text/css">
          p  {text-indent:200px;}
          li {text-indent:180px;}
          h1 {color:navy;}
          h2 {text-indent:50px; color:blueviolet}
          h3 {text-indent:100px;}
          h4 {text-indent:150px;}
        </style>
      </head>
      <body>
        <a name="top"/>
        <xsl:apply-templates select="//assembly"/>
      </body>
    </html>
  </xsl:template>

  <!-- ASSEMBLY TEMPLATE -->
  <!-- For each Assembly, display its name and then its member types -->
  <xsl:template match="assembly">
    <H1>
      <xsl:value-of select="name"/>
    </H1>
    
    <xsl:for-each select="//member[contains(@name,'T:')]">
      <xsl:sort select="substring-after(@name, '.')"/>
      <xsl:variable name="LinkMemberName"
        select="substring-after(@name, '.')"/>
      
      <a xmlns="http://www.w3.org/1999/xhtml">
        <xsl:attribute name="href">
          #<xsl:value-of select="$LinkMemberName"/>
        </xsl:attribute>
        <xsl:value-of select="$LinkMemberName"/>
      </a>
      <br xmlns="http://www.w3.org/1999/xhtml"/>
    </xsl:for-each>
    <xsl:apply-templates select="//member[contains(@name,'T:')]"/>
  </xsl:template>

  <!-- TYPE TEMPLATE -->
  <!-- Loop through member types and display their properties and methods -->
  <xsl:template match="//member[contains(@name,'T:')]">

    <!-- Two variables to make code easier to read -->
    <!-- A variable for the name of this type -->
    <xsl:variable name="MemberName"
        select="substring-after(@name, '.')"/>

    <!-- Get the type's fully qualified name without the T: prefix -->
    <xsl:variable name="FullMemberName"
                   select="substring-after(@name, ':')"/>

    <!-- Display the type's name and information -->
   
    <H2>
      <a xmlns="http://www.w3.org/1999/xhtml">
        <xsl:attribute name="name">
          <xsl:value-of select="$MemberName"/>
        </xsl:attribute>
        <xsl:value-of select="$MemberName"/>
      </a>
    </H2>
    
    <H3>Description</H3>
    <xsl:apply-templates/>

    <!-- If this type has public fields, display them -->
    <!-- <xsl:if test="//member[contains(@name,concat('F:',$FullMemberName))]">
   <H3>Fields</H3>

      <xsl:for-each select="//member[contains(@name,concat('F:',$FullMemberName))]">
        <H4><xsl:value-of select="substring-after(@name, concat('F:',$FullMemberName,'.'))"/></H4>
        <xsl:apply-templates/>
      </xsl:for-each>
  </xsl:if> -->

    <!-- If this type has properties, display them -->
    <xsl:if test="//member[contains(@name,concat('P:',$FullMemberName))]">
      <H3>Properties</H3>

      <xsl:for-each select="//member[contains(@name,concat('P:',$FullMemberName))]">
        <H4>
          <xsl:value-of select="substring-after(@name, concat('P:',$FullMemberName,'.'))"/>
        </H4>
        <xsl:apply-templates/>
      </xsl:for-each>
    </xsl:if>
    <a xmlns="http://www.w3.org/1999/xhtml" href="#top">Back To Index</a>
    <hr xmlns="http://www.w3.org/1999/xhtml"/>
    <!-- If this type has methods, display them -->
    <xsl:if test="//member[contains(@name,concat('M:',$FullMemberName))]">
      <H3>Methods</H3>

      <xsl:for-each select="//member[contains(@name,concat('M:',$FullMemberName))]">

        <!-- If this is a constructor, display the type name 
            (instead of "#ctor"), or display the method name -->
        <H4>
          <xsl:choose>
            <xsl:when test="contains(@name, '#ctor')">
              Constructor:
              <xsl:value-of select="$MemberName"/>
              <xsl:value-of select="substring-after(@name, '#ctor')"/>
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="substring-after(@name, concat('M:',$FullMemberName,'.'))"/>
            </xsl:otherwise>
          </xsl:choose>
        </H4>

        <xsl:apply-templates select="summary"/>

        <!-- Display parameters if there are any -->
        <xsl:if test="count(param)!=0">
          <H5>Parameters</H5>
          <xsl:apply-templates select="param"/>
        </xsl:if>

        <!-- Display return value if there are any -->
        <xsl:if test="count(returns)!=0">
          <H5>Return Value</H5>
          <xsl:apply-templates select="returns"/>
        </xsl:if>

        <!-- Display exceptions if there are any -->
        <xsl:if test="count(exception)!=0">
          <H5>Exceptions</H5>
          <xsl:apply-templates select="exception"/>
        </xsl:if>

        <!-- Display examples if there are any -->
        <xsl:if test="count(example)!=0">
          <H5>Example</H5>
          <xsl:apply-templates select="example"/>
        </xsl:if>

      </xsl:for-each>

    </xsl:if>
  </xsl:template>

  <!-- OTHER TEMPLATES -->
  <!-- Templates for other tags -->
  <xsl:template match="preconditions">
    <H4>Required System State Before</H4>
    <P>
      <!-- xsl:apply-templates / -->
      <xsl:value-of select="." disable-output-escaping="yes"/>
    </P>
  </xsl:template>

  <xsl:template match="postconditions">
    <H4>System State After Test Case</H4>
    <P>
      <xsl:apply-templates />
    </P>
  </xsl:template>

  <xsl:template match="workflow">
    <H4>Workflow</H4>
    <P>
      <UL>
        <xsl:apply-templates />
      </UL>
    </P>
  </xsl:template>

  <xsl:template match="action">
    <LI style="color: #0000ff">
      Action: <xsl:apply-templates />
    </LI>
  </xsl:template>

  <xsl:template match="verification">
    <LI style="color: #009900">
      Verification: <xsl:apply-templates />
    </LI>
  </xsl:template>

  <!-- <xsl:template match="a">
  <A href="{@href}">
    <xsl:apply-templates />
  </A>
</xsl:template> -->

  <xsl:template match="c">
    <CODE>
      <xsl:apply-templates />
    </CODE>
  </xsl:template>

  <xsl:template match="code">
    <PRE>
      <xsl:apply-templates />
    </PRE>
  </xsl:template>

  <xsl:template match="example">
    <P>
      <STRONG>Example: </STRONG>
      <xsl:apply-templates />
    </P>
  </xsl:template>

  <xsl:template match="exception">
    <P>
      <STRONG>
        <xsl:value-of select="substring-after(@cref,'T:')"/>:
      </STRONG>
      <xsl:apply-templates />
    </P>
  </xsl:template>

  <xsl:template match="include">
    <A HREF="{@file}">External file</A>
  </xsl:template>

  <xsl:template match="para">
    <P>
      <xsl:apply-templates />
    </P>
  </xsl:template>

  <xsl:template match="param">
    <P>
      <STRONG>
        <xsl:value-of select="@name"/>:
      </STRONG>
      <xsl:apply-templates />
    </P>
  </xsl:template>

  <xsl:template match="paramref">
    <EM>
      <xsl:value-of select="@name" />
    </EM>
  </xsl:template>

  <xsl:template match="permission">
    <P>
      <STRONG>Permission: </STRONG>
      <EM>
        <xsl:value-of select="@cref" />
      </EM>
      <xsl:apply-templates />
    </P>
  </xsl:template>

  <xsl:template match="remarks">
    <P>
      <i>
        <STRONG>Remarks: </STRONG>
        <xsl:value-of select="." disable-output-escaping="yes"/>
      </i>
    </P>
  </xsl:template>

  <xsl:template match="returns">
    <P>
      <STRONG>Return Value: </STRONG>
      <xsl:apply-templates />
    </P>
  </xsl:template>

  <xsl:template match="see">
    <EM>
      See: <xsl:value-of select="@cref" />
    </EM>
  </xsl:template>

  <xsl:template match="seealso">
    <EM>
      See also: <xsl:value-of select="@cref" />
    </EM>
  </xsl:template>

  <xsl:template match="summary">
    <P>
      <xsl:apply-templates />
    </P>
  </xsl:template>

  <xsl:template match="list">
    <xsl:choose>
      <xsl:when test="@type='bullet'">
        <UL>
          <xsl:for-each select="listheader">
            <LI>
              <strong>
                <xsl:value-of select="term"/>:
              </strong>
              <xsl:value-of select="definition"/>
            </LI>
          </xsl:for-each>
          <xsl:for-each select="list">
            <LI>
              <strong>
                <xsl:value-of select="term"/>:
              </strong>
              <xsl:value-of select="definition"/>
            </LI>
          </xsl:for-each>
        </UL>
      </xsl:when>
      <xsl:when test="@type='number'">
        <OL>
          <xsl:for-each select="listheader">
            <LI>
              <strong>
                <xsl:value-of select="term"/>:
              </strong>
              <xsl:value-of select="definition"/>
            </LI>
          </xsl:for-each>
          <xsl:for-each select="list">
            <LI>
              <strong>
                <xsl:value-of select="term"/>:
              </strong>
              <xsl:value-of select="definition"/>
            </LI>
          </xsl:for-each>
        </OL>
      </xsl:when>
      <xsl:when test="@type='table'">
        <TABLE>
          <xsl:for-each select="listheader">
            <TH>
              <TD>
                <xsl:value-of select="term"/>
              </TD>
              <TD>
                <xsl:value-of select="definition"/>
              </TD>
            </TH>
          </xsl:for-each>
          <xsl:for-each select="list">
            <TR>
              <TD>
                <strong>
                  <xsl:value-of select="term"/>:
                </strong>
              </TD>
              <TD>
                <xsl:value-of select="definition"/>
              </TD>
            </TR>
          </xsl:for-each>
        </TABLE>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

</xsl:stylesheet>
