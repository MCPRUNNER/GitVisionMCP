<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="xml" indent="yes"/>
    
    <xsl:template match="/">
        <transformed>
            <xsl:for-each select="catalog/book">
                <book-item>
                    <id><xsl:value-of select="@id"/></id>
                    <title><xsl:value-of select="title"/></title>
                    <author><xsl:value-of select="author"/></author>
                </book-item>
            </xsl:for-each>
        </transformed>
    </xsl:template>
</xsl:stylesheet>
