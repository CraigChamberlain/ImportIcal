#Requires -Module Pester

Describe 'New-IcalOrganizer' {
    It 'Given all parameters' {
              $org = New-IcalOrganizer `
                        -SentBy "mailto:bob@example" `
                        -CommonName Bob `
                        -DirectoryEntry "http://www.resouce.com/thing" `
                        -Language "En"

            $org.SentBy | Should -Be "mailto:bob@example" 
            $org.CommonName | Should -Be "Bob" 
            $org.DirectoryEntry | Should -Be "http://www.resouce.com/thing" 
            $org.Language | Should -Be "En"
    }
     It 'Given minimum parameters' {

        $org = New-IcalOrganizer 

        $org.SentBy | Should -BeNullOrEmpty 
        $org.CommonName | Should -BeNullOrEmpty
        $org.DirectoryEntry | Should -BeNullOrEmpty
        $org.Language | Should -BeNullOrEmpty

    }
}